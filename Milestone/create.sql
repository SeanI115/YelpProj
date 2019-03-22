CREATE TABLE business (
    business_id VARCHAR(25) NOT NULL,
    name VARCHAR(100),
    state VARCHAR(2),
    city VARCHAR(40),
    zipcode INT,
    latitude FLOAT,
    longitude FLOAT,
    address VARCHAR(100),
    review_count INT,
    num_checkins INT,
    reviewRating FLOAT,
    is_open BOOLEAN,
    stars FLOAT,
    PRIMARY KEY(business_id)
);


CREATE TABLE hours (
    day VARCHAR(15) NOT NULL,
    business_id VARCHAR(25),
    close VARCHAR(20),
    open VARCHAR(20),
    PRIMARY KEY(day, business_id),
    FOREIGN KEY (business_id) REFERENCES business ON DELETE CASCADE
);


CREATE TABLE categories (
    category_name VARCHAR(30),
    business_id VARCHAR(25),
    PRIMARY KEY (category_name, business_id),
    FOREIGN KEY (business_id) REFERENCES business 
        ON DELETE CASCADE
);



CREATE TABLE checkins (
    day VARCHAR(15) NOT NULL,
    business_id VARCHAR(25),
    time vARCHAR(20) NOT NULL,
    count INT,
    PRIMARY KEY(business_id, day, time),
    FOREIGN KEY(business_id) REFERENCES business ON DELETE CASCADE
);

CREATE TABLE acc (
    average_stars FLOAT,
    compliment_cool INT,
    compliment_cute INT,
    compliment_funny INT,
    compliment_hot INT,
    compliment_list INT,
    compliment_more INT,
    compliment_not INT,
    compliment_photos INT,
    compliment_plain INT,
    compliment_profile INT,
    compliment_writer INT,
    cool INT,
    fans INT,
    funny INT,
    name VARCHAR(25),
    review_count INT,
    useful INT,
    user_id VARCHAR(25) NOT NULL,
    yelping_since VARCHAR(10),
    PRIMARY KEY(user_id)
);


CREATE TABLE friends (
    friend_id VARCHAR(25),
    user_id VARCHAR(25),
    PRIMARY KEY(friend_id),
    FOREIGN KEY(user_id) references acc
);

CREATE TABLE review (
    review_id VARCHAR(22) NOT NULL,
    user_id VARCHAR(25) NOT NULL,
    business_id VARCHAR(25) NOT NULL,
    stars INT,
    date VARCHAR(10),
    text VARCHAR(100),
    useful_vote INT,
    funny_vote INT,
    cool_vote INT,
    PRIMARY KEY(review_id),
    FOREIGN KEY(business_id) REFERENCES business,
    FOREIGN KEY(user_id) REFERENCES acc
);