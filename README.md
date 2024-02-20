# BookApp
In the database, 4 tables have been saved: Author, Book, BookAuthor, Loan. In the Author table, only the author's last name, first name, and ID are saved. In the Book table, the publication year, title, and ID are saved. In the BookAuthor table, the author's ID and the book's ID are saved. These two IDs are foreign keys. In the Loan table, the following columns are included: Id, BookId, Email, IsFree, DateOfBorrowing, DeadLine. In this table, BookId is also a foreign key. DateOfBorrowing is saved as the daily date, while DeadLine is three months later than DateOfBorrowing.
## Trying out the app
If you want to try out how the application works, you need to take the following steps:
1. Open Docker Desktop.
2. Open the command prompt and navigate to the root directory where the Program.cs file is located. (Alternatively, open the project in VS Code and click on the Terminal tab.)
3. docker-compose build
4. docker-compose up -d
5. If you want to access the database through the console, you can do so as follows: docker-compose exec postgresql psql -U myuser -d mydatabase