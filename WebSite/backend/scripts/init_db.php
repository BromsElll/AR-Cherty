<?php
require_once __DIR__ . '/../src/Database.php';

use App\Database;

$db = new Database(__DIR__ . '/../data/database.sqlite');
$pdo = $db->getPdo();

$pdo->exec("CREATE TABLE IF NOT EXISTS posts (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    content TEXT NOT NULL,
    created_at TEXT NOT NULL
);");

echo "Database initialized at data/database.sqlite\n";
