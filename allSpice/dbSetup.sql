CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL COMMENT 'Recipe Title',
  instructions VARCHAR(255) NOT NULL COMMENT 'Recipe Instructions',
  img VARCHAR(700) NOT NULL COMMENT 'Recipe Image',
  category ENUM('Cheese', 'Italian', 'Soup', 'Mexican', 'Specialty Coffee', 'Snacks'),
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
) default charset utf8 COMMENT '';

DROP TABLE recipes;

INSERT INTO recipes(title, instructions, img, category, creatorId)
VALUES ('Avocado Rice Cake', '1. Mash Avocado to smooth but chunky consistency, 2. Spread on desired ricecake, 3. Top with Everything seasoning, pepperflakes, lemon juice/zest', 'https://images.unsplash.com/photo-1603046891726-36bfd957e0bf?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8YXZvY2FkbyUyMHRvYXN0fGVufDB8fDB8fHww&auto=format&fit=crop&w=500&q=60', 'Snacks', '64adb7d638a6f807f070aea8');

SELECT
rec.*,
acc.*
FROM recipes rec 
JOIN accounts acc
ON acc.id = rec.creatorId;

SELECT
rec.*,
acc.*
FROM recipes rec 
JOIN accounts acc ON acc.id = rec.creatorId
WHERE rec.id = 2 LIMIT 1;
