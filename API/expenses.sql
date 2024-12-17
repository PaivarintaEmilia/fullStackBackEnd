CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Email" TEXT NOT NULL UNIQUE,
    "Role" TEXT NOT NULL,
    "PasswordSalt" BLOB NOT NULL,
    "HashedPassword" BLOB NOT NULL
);

CREATE TABLE "Incomes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Incomes" PRIMARY KEY AUTOINCREMENT,
    "Amount" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "CreatedAt" DATETIME DEFAULT CURRENT_TIMESTAMP,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_Incomes_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);


CREATE TABLE "Expenses" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Expenses" PRIMARY KEY AUTOINCREMENT,
    "Amount" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "CreatedAt" DATETIME DEFAULT CURRENT_TIMESTAMP,
    "UserId" INTEGER NOT NULL,
    "CategoryId" INTEGER NOT NULL,
    CONSTRAINT "FK_Expenses_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    CONSTRAINT "FK_Expenses_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id")
);

CREATE TABLE "Categories" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "UserDefined" BOOLEAN DEFAULT FALSE,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_Categories_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE 
);




CREATE INDEX "IX_Incomes_UserId" ON "Incomes" ("UserId");

CREATE INDEX "IX_Expenses_UserId" ON "Expenses" ("UserId");

CREATE INDEX "IX_Expenses_CategoryId" ON "Expenses" ("CategoryId");

CREATE INDEX "IX_Categories_UserId" ON "Categories" ("UserId");


INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241015105356_Initial', '8.0.8');

COMMIT;

