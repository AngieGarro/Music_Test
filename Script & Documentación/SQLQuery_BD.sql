-- Crear la base de datos
CREATE DATABASE Bd_TestMusic;

-- Usar la base de datos
USE Bd_TestMusic;

-- Creación de Tablas
CREATE TABLE PlayList (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    createDate DATETIME DEFAULT GETDATE()
);


CREATE TABLE Song (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(255) NOT NULL,
    artistName NVARCHAR(255),
    album NVARCHAR(255),
    duration TIME
);

-- Tabla secundaria para ingresar canciones a una playlist

CREATE TABLE PlayList_Songs (
    id_playlist INT,
    id_song INT,
    PRIMARY KEY (id_playlist, id_song),
    FOREIGN KEY (id_playlist) REFERENCES PlayList(id),
    FOREIGN KEY (id_song) REFERENCES Song(id)
);

-- STORED PROCEDURES PLAYLIST

------------------------------------------------------
CREATE PROCEDURE CREATE_PLAYLIST_PR
    @P_NAME NVARCHAR(255),
    @P_CREATE_DATE DATETIME = NULL
AS
BEGIN

    IF @P_CREATE_DATE IS NULL
    BEGIN
        SET @P_CREATE_DATE = GETDATE();
    END

    -- Inserta el registro en la tabla PlayList
    INSERT INTO PlayList (name, createDate)
    VALUES (@P_NAME, @P_CREATE_DATE);
END

-- Testing procedure 
EXEC CREATE_PLAYLIST_PR @P_NAME  = 'Chill Vibes';

SELECT * FROM PlayList;

---------------------------------------------------------------
CREATE PROCEDURE RET_ALL_PLAYLIST_PR
AS
BEGIN
    SELECT id, name, createDate FROM PlayList;
END

-- Testing procedure
EXEC RET_ALL_PLAYLIST_PR;

----------------------------------------------------------------
CREATE PROCEDURE DEL_PLAYLIST_PR
    @P_ID INT
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Verifica si existe la playlist
        IF EXISTS (SELECT 1 FROM PlayList WHERE id = @P_ID)
        BEGIN
            -- Elimina todas las canciones de la playlist
            DELETE FROM PlayList_Songs WHERE id_playlist = @P_ID;

            -- Elimina la playlist
            DELETE FROM PlayList WHERE id = @P_ID;

            PRINT 'Playlist eliminada correctamente.';
        END
        ELSE
        BEGIN
            PRINT 'No se encontró la playlist con el ID especificado.';
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT 'Ocurrió un error al eliminar la playlist.';
    END CATCH
END


-- Testing procedure
EXEC DEL_PLAYLIST_PR @P_ID = 1;

---------------------------------------------------

CREATE PROCEDURE UPD_PLAYLIST_PR
     @P_ID INT,
    @P_NAME NVARCHAR(255)
AS
BEGIN

    IF EXISTS (SELECT 1 FROM PlayList WHERE id = @P_ID)
    BEGIN
        UPDATE PlayList
        SET name = @P_NAME
        WHERE id = @P_ID;
        PRINT 'Nombre de la playlist actualizado correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'No se encontró la playlist con el ID especificado.';
    END
END

-- Testing Procedure
EXEC UPD_PLAYLIST_PR @P_ID = 2, @P_NAME = 'New Chill Vibes';

--------------------------------------------------------------------

-- STORED PROCEDURES SONGS

CREATE PROCEDURE CREATE_SONGS_PR
    @P_TITLE NVARCHAR(255),
    @P_ARTIST_NAME NVARCHAR(255) = NULL,
    @P_ALBUM NVARCHAR(255) = NULL,
    @P_DURATION TIME = NULL
AS
BEGIN

    INSERT INTO Song (title, artistName, album, duration)
    VALUES (@P_TITLE, @P_ARTIST_NAME, @P_ALBUM, @P_DURATION);
    
    PRINT 'Canción insertada correctamente.';
END

--Testing Procedure
EXEC CREATE_SONGS_PR @P_TITLE = 'FIRST', @P_ARTIST_NAME = 'NJJ', @P_ALBUM = 'Extreme', @P_DURATION= '00:03:49';

SELECT * FROM Song;

------------------------------------------------------------------------------------
CREATE PROCEDURE RET_ALL_SONGS_PR
AS
BEGIN
    -- Selecciona y devuelve todos los registros de la tabla Song
    SELECT id, title, artistName, album, duration FROM Song;
END

EXEC RET_ALL_SONGS_PR;

-- STORED PROCEDURES PLAYLIST --> SONG

CREATE PROCEDURE ADD_SongToPlayList
    @P_id_playlist INT,
    @P_id_song INT
AS
BEGIN

    IF NOT EXISTS (SELECT 1 FROM PlayList_Songs WHERE id_playlist = @P_id_playlist AND id_song = @P_id_song)
    BEGIN
        INSERT INTO PlayList_Songs (id_playlist, id_song)
        VALUES (@P_id_playlist, @P_id_song);
        PRINT 'Canción insertada en la playlist correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'La canción ya existe en la playlist.';
    END
END

-- Testing Procedure
EXEC ADD_SongToPlayList @P_id_playlist = 3, @P_id_song = 2;

SELECT * FROM PlayList_Songs;

SELECT * FROM PlayList;

SELECT * FROM Song;

-----------------------------------------------------------------------------------
CREATE PROCEDURE LIST_SongsInPlaylist
    @P_id_playlist INT
AS
BEGIN
    SELECT ps.id_playlist, pl.name, id_song, s.title, s.artistName, s.album, s.duration
    FROM PlayList_Songs ps
    INNER JOIN Song s ON ps.id_song = s.id
	INNER JOIN PlayList pl on ps.id_playlist = pl.id
    WHERE ps.id_playlist = @P_id_playlist;
END

-- Testing Procedure
EXEC LIST_SongsInPlayList @P_id_playlist = 3;

-----------------------------------------------------------

CREATE PROCEDURE REMOVE_SongFromPlaylist
    @P_id_playlist INT,
    @P_id_song INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM PlayList_Songs WHERE id_playlist = @P_id_playlist AND id_song = @P_id_song)
    BEGIN
        DELETE FROM PlayList_Songs 
        WHERE id_playlist = @P_id_playlist AND id_song = @P_id_song;
        PRINT 'Canción eliminada de la playlist correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'La canción no se encontró en la playlist.';
    END
END

-- Testing Procedure
EXEC REMOVE_SongFromPlayList @P_id_playlist = 2, @P_id_song = 1;

SELECT * FROM PlayList_Songs;

EXEC LIST_SongsInPlayList @P_id_playlist = 2;

-- INSERTS PARA LA TABLA SONG
INSERT INTO Song (title, artistName, album, duration) VALUES
('Thriller', 'Michael Jackson', 'Thriller', '00:05:57'),
('Purple Haze', 'Jimi Hendrix', 'Are You Experienced', '00:02:50'),
('Born to Run', 'Bruce Springsteen', 'Born to Run', '00:04:30'),
('Good Vibrations', 'The Beach Boys', 'Smiley Smile', '00:03:36'),
('Comfortably Numb', 'Pink Floyd', 'The Wall', '00:06:21'),
('Hotel California', 'Eagles', 'Hotel California', '00:06:30'),
('Superstition', 'Stevie Wonder', 'Talking Book', '00:04:26'),
('Let It Be', 'The Beatles', 'Let It Be', '00:04:03'),
('Hallelujah', 'Leonard Cohen', 'Various Positions', '00:04:39'),
('Back in Black', 'AC/DC', 'Back in Black', '00:04:15'),
('Lose Yourself', 'Eminem', '8 Mile', '00:05:26'),
('One', 'U2', 'Achtung Baby', '00:04:36'),
('Respect', 'Aretha Franklin', 'I Never Loved a Man the Way I Love You', '00:02:29'),
('Billie Jean', 'Michael Jackson', 'Thriller', '00:04:54'),
('Bridge Over Troubled Water', 'Simon & Garfunkel', 'Bridge Over Troubled Water', '00:04:55'),
('No Woman, No Cry', 'Bob Marley and the Wailers', 'Live!', '00:07:07'),
('Bohemian Rhapsody', 'Queen', 'A Night at the Opera', '00:05:55'),
('A Day in the Life', 'The Beatles', 'Sgt. Pepper s Lonely Hearts Club Band', '00:05:33'),
('American Pie', 'Don McLean', 'American Pie', '00:08:36'),
('Smells Like Teen Spirit', 'Nirvana', 'Nevermind', '00:05:01'),
('Imagine', 'John Lennon', 'Imagine', '00:03:04'),
('Light My Fire', 'The Doors', 'The Doors', '00:07:08'),
('Whats Going On', 'Marvin Gaye', 'Whats Going On', '00:03:53'),
('Rolling in the Deep', 'Adele', '21', '00:03:48'),
('Whole Lotta Love', 'Led Zeppelin', 'Led Zeppelin II', '00:05:33'),
('Hotel California', 'Eagles', 'Hotel California', '00:06:30'),
('Satisfaction', 'The Rolling Stones', 'Out of Our Heads', '00:03:43'),
('Sweet Child o Mine', 'Guns Ns Roses', 'Appetite for Destruction', '00:05:56'),
('Wonderwall', 'Oasis', '(Whats the Story) Morning Glory?', '00:04:18'),
('Like a Rolling Stone', 'Bob Dylan', 'Highway 61 Revisited', '00:06:09'),
('Sympathy for the Devil', 'The Rolling Stones', 'Beggars Banquet', '00:06:27'),
('Enter Sandman', 'Metallica', 'Metallica', '00:05:32'),
('Losing My Religion', 'R.E.M.', 'Out of Time', '00:04:28'),
('Clocks', 'Coldplay', 'A Rush of Blood to the Head', '00:05:07'),
('With or Without You', 'U2', 'The Joshua Tree', '00:04:56'),
('Every Breath You Take', 'The Police', 'Synchronicity', '00:04:13'),
('Heroes', 'David Bowie', 'Heroes', '00:06:07'),
('Space Oddity', 'David Bowie', 'David Bowie', '00:05:18'),
('Hotel California', 'Eagles', 'Hotel California', '00:06:30');
