CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- 62fead19fda8e818d13a81db
-- 632e194f96fb01c2672f465c

CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500),
  img VARCHAR(1000) NOT NULL,
  views INT DEFAULT 0,
  shares INT DEFAULT 0,
  vaultKeepId INT,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO keeps
(creatorId, name, img)
VALUE
("632e194f96fb01c2672f465c", "cat3", "https://www.rd.com/wp-content/uploads/2020/07/35_No-regrets-1.jpg?fit=700,700");

CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(100) NOT NULL,
  description VARCHAR(500),
  img VARCHAR(1000) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaults
(creatorId, name, img)
VALUES
("632e194f96fb01c2672f465c", "funny", "https://assets3.thrillist.com/v1/image/2824030/1200x630/flatten;crop_down;webp=auto;jpeg_quality=70");

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaultKeeps
(creatorId, vaultId, keepId)
VALUES
("62fead19fda8e818d13a81db", "1", "3");

SELECT * FROM keeps ORDER BY id desc;

SELECT 
COUNT(vaultId) AS kept,
k.*,
a.*
FROM keeps k LEFT JOIN vaultKeeps vk ON k.id = vk.keepId JOIN accounts a ON k.creatorId = a.id WHERE k.id = "3";

SELECT * FROM vaults;