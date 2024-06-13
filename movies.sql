CREATE TABLE Movie(
	IdMovie INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(100) not null,
	MovieDescription NVARCHAR(MAX) not null,	
	Duration NVARCHAR(10) not null,
	Link NVARCHAR(300) not null,
	Picture varbinary(max) null
)

CREATE TABLE Actor(
	IdActor INT PRIMARY KEY IDENTITY,
	ActorName NVARCHAR(50) not null
)

CREATE TABLE Director(
	IdDirector INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(50) not null
)

CREATE TABLE MovieActor
(
	IdMovieActor INT PRIMARY KEY IDENTITY,
	MovieId INT not null FOREIGN KEY REFERENCES Movie(IdMovie),
	ActorId INT null FOREIGN KEY REFERENCES Actor(IdActor)
)

CREATE TABLE MovieDirector
(
	IdMovieDirector INT PRIMARY KEY IDENTITY,
	MovieId INT not null FOREIGN KEY REFERENCES Movie(IdMovie),
	DirectorId INT null FOREIGN KEY REFERENCES Director(IdDirector)
)
GO

--movie
CREATE PROC GetMovies
AS
SELECT * FROM Movie
GO

CREATE PROC GetMovie
	@IdMovie INT
AS
SELECT * FROM Movie WHERE IdMovie = @IdMovie
GO

CREATE PROC DeleteMovieActors
	@MovieId INT
AS
BEGIN
	DELETE FROM MovieActor
	WHERE MovieId = @MovieId
END
GO

CREATE PROC DeleteMovieDirectors
	@MovieId INT
AS
BEGIN
	DELETE FROM MovieDirector
	WHERE MovieId = @MovieId
END
GO

CREATE PROC DeleteMovie
	@MovieId INT
AS
BEGIN
	DELETE FROM MovieActor WHERE MovieId = @MovieId
	DELETE FROM MovieDirector WHERE MovieId = @MovieId
	DELETE FROM Movie WHERE IdMovie = @MovieId
END
GO

CREATE PROC AddMovie
	@Title NVARCHAR(100),
	@MovieDescription NVARCHAR(MAX),
	@Duration NVARCHAR(10),
	@Link NVARCHAR(300),
	@Picture varbinary(max),
	@IdMovie INT OUTPUT
AS
BEGIN 
	INSERT INTO Movie VALUES(@Title, @MovieDescription, @Duration, @Link, @Picture)
	SET @IdMovie = SCOPE_IDENTITY()
END
GO

CREATE PROC SetMovieDirectors
	@MovieId INT,
	@IdDirector INT
AS
BEGIN
	INSERT INTO MovieDirector VALUES (@MovieId, @IdDirector) 
	SELECT * FROM MovieDirector
	WHERE MovieId = @MovieId
END
GO

CREATE PROC SetMovieActors
	@MovieId INT,
	@IdActor INT
AS
BEGIN
	INSERT INTO MovieActor VALUES (@MovieId, @IdActor) 
	SELECT * FROM MovieActor
	WHERE MovieId = @MovieId
END
GO

CREATE PROC UpdateMovie
	@Title NVARCHAR(100),
	@MovieDescription NVARCHAR(MAX),	
	@Duration NVARCHAR(10),
	@Link NVARCHAR(300),
	@Picture varbinary(max),
	@IdMovie INT
AS
UPDATE Movie SET 
		Title = @Title, 
		MovieDescription = @MovieDescription, 
		Duration = @Duration,  
		Link = @Link,
		Picture = @Picture
	WHERE 
		IdMovie = @IdMovie
GO

--actor
CREATE PROC GetActors
AS
SELECT * FROM Actor
GO

CREATE PROC GetActor
	@IdActor INT
AS
SELECT * FROM Actor WHERE IdActor = @IdActor
GO

CREATE PROC DeleteActor
	@IdActor INT
AS
DELETE FROM Actor WHERE IdActor = @IdActor
GO

CREATE PROC AddActor
	@ActorName NVARCHAR(50),
	@IdActor INT OUTPUT
AS
BEGIN
	INSERT INTO Actor VALUES (@ActorName) 
	SET @IdActor = SCOPE_IDENTITY()
END
GO

CREATE PROC AddMovieActor
	@MovieId INT,
	@ActorName NVARCHAR(50),
	@IdActor INT OUTPUT
AS
BEGIN
	INSERT INTO Actor VALUES (@ActorName)
	SET @IdActor = SCOPE_IDENTITY()
	INSERT INTO MovieActor VALUES (@MovieId, @IdActor)
END
GO

CREATE PROC GetMovieActors
	@IdMovie INT
AS
BEGIN
	SELECT a.IdActor, a.ActorName FROM Actor a
	join MovieActor mc ON mc.ActorId=a.IdActor
	join Movie m ON m.IdMovie=mc.MovieId
	WHERE IdMovie = @IdMovie
END
GO

CREATE PROC UpdateActor
	@ActorName NVARCHAR(50),
	@IdActor INT
AS
UPDATE Actor SET 
		ActorName = @ActorName
	WHERE 
		IdActor = @IdActor
GO

CREATE PROC ActorMovieRelated
	@IdActor INT,
	@returnCode INT OUTPUT
AS
BEGIN
	DECLARE @count INT
	SELECT @count = COUNT(ActorId) FROM MovieActor WHERE ActorId = @IdActor
	IF @count = 0
		BEGIN
			SET @returnCode = 0
		END
	ELSE
		BEGIN
			SET @returnCode = -1
		END
END
GO

--director
CREATE PROC GetDirectors
AS
SELECT * FROM Director
GO

CREATE PROC GetDirector
	@IdDirector INT
AS
SELECT * FROM Director WHERE IdDirector = @IdDirector
GO

CREATE PROC DeleteDirector
	@IdDirector INT
AS
DELETE FROM Director WHERE IdDirector = @IdDirector
GO

CREATE PROC AddDirector
	@DirectorName NVARCHAR(50),
	@IdDirector INT OUTPUT
AS
BEGIN
	INSERT INTO Director VALUES (@DirectorName) 
	SET @IdDirector = SCOPE_IDENTITY()
END
GO

CREATE PROC AddMovieDirector
	@MovieId INT,
	@DirectorName NVARCHAR(50),
	@IdDirector INT OUTPUT
AS
BEGIN 
	INSERT INTO Director VALUES (@DirectorName)
	SET @IdDirector = SCOPE_IDENTITY()
	INSERT INTO MovieDirector VALUES (@MovieId, @IdDirector)
END
GO

CREATE PROC GetMovieDirectors
	@IdMovie INT
AS
BEGIN
	SELECT d.IdDirector, d.DirectorName FROM Director d
	join MovieDirector md ON md.DirectorId=d.IdDirector
	join Movie m ON m.IdMovie=md.MovieId
	WHERE IdMovie = @IdMovie
END
GO

CREATE PROC UpdateDirector
	@DirectorName NVARCHAR(50),
	@IdDirector INT
AS
UPDATE Director SET 
		DirectorName = @DirectorName
	WHERE 
		IDDirector = @IdDirector
GO

CREATE PROC DirectorMovieRelated
	@IdDirector INT,
	@returnCode INT output
AS
BEGIN
	DECLARE @count INT
	SELECT @count = COUNT(DirectorId) FROM MovieDirector WHERE DirectorId = @IdDirector
	IF @count = 0
		BEGIN
			SET @returnCode = 0
		END
	ELSE
		BEGIN
			SET @returnCode = -1
		END
END
