CREATE TABLE IF NOT EXISTS "Author" (
    "Id" SERIAL PRIMARY KEY,
    "Surname" VARCHAR(255) NOT NULL,
    "FirstName" VARCHAR(255) NOT NULL
);
CREATE TABLE IF NOT EXISTS "Book" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "PublicationYear" INT
);
CREATE TABLE IF NOT EXISTS "BookAuthor" (
    "Id" SERIAL PRIMARY KEY,
    "BookId" INT,
    "AuthorId" INT,
    FOREIGN KEY ("BookId") REFERENCES "Book"("Id"),
    FOREIGN KEY ("AuthorId") REFERENCES "Author"("Id")
);
CREATE TABLE IF NOT EXISTS "Loan" (
    "Id" serial PRIMARY KEY,
    "BookId" INT,
    "Email" VARCHAR(255) NOT NULL,
    "IsFree" BOOLEAN DEFAULT FALSE,
    "DateOfBorowing" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "DeadLine" TIMESTAMP DEFAULT (CURRENT_TIMESTAMP + INTERVAL '90 days'),
    FOREIGN KEY ("BookId") REFERENCES "Book"("Id")
);
