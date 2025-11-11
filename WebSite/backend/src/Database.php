<?php
namespace App;

class Database {
    private \PDO $pdo;

    public function __construct(string $path) {
        $dir = dirname($path);
        if (!is_dir($dir)) mkdir($dir, 0755, true);
        $this->pdo = new \PDO('sqlite:' . $path);
        $this->pdo->setAttribute(\PDO::ATTR_ERRMODE, \PDO::ERRMODE_EXCEPTION);
        $this->pdo->exec('PRAGMA foreign_keys = ON;');
    }

    public function getPdo(): \PDO {
        return $this->pdo;
    }
}
