USE AnnouncementsDb;
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;

CREATE FULLTEXT INDEX ON Announcements(Title LANGUAGE English, Description LANGUAGE English)
KEY INDEX PK_Announcements -- This should be the name of the Primary Key constraint on targeted table
ON ftCatalog; -- This is the catalog we created in the previous step