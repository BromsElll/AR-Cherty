<?php
require_once __DIR__ . '/../src/helpers.php';
require_once __DIR__ . '/../src/Database.php';

use App\Database;

// CORS
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS");
header("Access-Control-Allow-Headers: Content-Type, Authorization");
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(204);
    exit;
}

$db = new Database(__DIR__ . '/../data/database.sqlite');
$pdo = $db->getPdo();

$path = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH);
$method = $_SERVER['REQUEST_METHOD'];

$segments = array_values(array_filter(explode('/', $path)));

if (count($segments) >= 2 && $segments[0] === 'api' && $segments[1] === 'posts') {
    $id = $segments[2] ?? null;
    handle_posts($pdo, $method, $id);
} else {
    echo "<h1>PHP Backend</h1><p>Use the API under /api/posts</p>";
}
