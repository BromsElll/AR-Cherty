<?php

function json_response($data, $code = 200) {
    header('Content-Type: application/json');
    http_response_code($code);
    echo json_encode($data, JSON_UNESCAPED_UNICODE);
    exit;
}

function get_json_body() {
    $body = file_get_contents('php://input');
    $data = json_decode($body, true);
    return $data ?: [];
}

function handle_posts(PDO $pdo, $method, $id = null) {
    switch ($method) {
        case 'GET':
            if ($id) {
                $stmt = $pdo->prepare('SELECT * FROM posts WHERE id = :id');
                $stmt->execute([':id' => $id]);
                $post = $stmt->fetch(PDO::FETCH_ASSOC);
                json_response($post ?: ['error' => 'Not found'], $post ? 200 : 404);
            } else {
                $stmt = $pdo->query('SELECT * FROM posts ORDER BY created_at DESC');
                $posts = $stmt->fetchAll(PDO::FETCH_ASSOC);
                json_response($posts);
            }
            break;
        case 'POST':
            $data = get_json_body();
            if (empty($data['title']) || empty($data['content'])) {
                json_response(['error' => 'title and content required'], 400);
            }
            $stmt = $pdo->prepare('INSERT INTO posts (title, content, created_at) VALUES (:title, :content, :created_at)');
            $stmt->execute([
                ':title' => $data['title'],
                ':content' => $data['content'],
                ':created_at' => date('c')
            ]);
            $id = $pdo->lastInsertId();
            $stmt = $pdo->prepare('SELECT * FROM posts WHERE id = :id');
            $stmt->execute([':id' => $id]);
            $post = $stmt->fetch(PDO::FETCH_ASSOC);
            json_response($post, 201);
            break;
        case 'PUT':
            if (!$id) json_response(['error' => 'id required'], 400);
            $data = get_json_body();
            $stmt = $pdo->prepare('UPDATE posts SET title = :title, content = :content WHERE id = :id');
            $stmt->execute([
                ':title' => $data['title'] ?? null,
                ':content' => $data['content'] ?? null,
                ':id' => $id
            ]);
            $stmt = $pdo->prepare('SELECT * FROM posts WHERE id = :id');
            $stmt->execute([':id' => $id]);
            $post = $stmt->fetch(PDO::FETCH_ASSOC);
            json_response($post ?: ['error' => 'Not found'], $post ? 200 : 404);
            break;
        case 'DELETE':
            if (!$id) json_response(['error' => 'id required'], 400);
            $stmt = $pdo->prepare('DELETE FROM posts WHERE id = :id');
            $stmt->execute([':id' => $id]);
            json_response(['deleted' => (int)$stmt->rowCount()]);
            break;
        default:
            json_response(['error' => 'Method not allowed'], 405);
    }
}
