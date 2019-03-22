CREATE TABLE Account(
    userId      CHAR(20)
    primary key (userID)   
)

CREATE TABLE ProfileInfo(   
    name        CHAR(20)
    reviewCount INTEGER
    avgStars    INTEGER
    fans        INTEGER
    longitute   INTEGER
    latitude    INTEGER
    userId      CHAR(20)
    foreign key (userId) references Account
    primary key (name, userId)
)

CREATE TABLE Friends(
    name        CHAR(20)
    dateJoined  CHAR(20)
    starRating  FLOAT
    primary key(name)
)

CREATE TABLE Business(
    name        CHAR(20)
    avgStars    FLOAT
    stars       INTEGER
    streetNum   INTEGER
    street      CHAR(20)
    state       CHAR(20)
    city        CHAR(20)
    primary key(name)
)

CREATE TABLE Review(
    name        CHAR(20)
    revText     CHAR(100)
    stars       FLOAT
    foreign key (stars) references Business
    foreign key (name) references Business
    primary key (name)


)

CREATE TABLE FavoriteBusinesses(    
    avgRating   INTEGER
    name        CHAR(20)
    city        CHAR(20)
    zipcode     INTEGER
    foreign key (name) references Business
    foreign key (avgRating) references Business
    foreign key (city) references Business
    primary key (name)
)




