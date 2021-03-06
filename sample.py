#import python
import psycopg2
import json

def cleanStr4SQL(s):
    return s.replace("'", "`").replace("\n", " ")


def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('C:/Users/Jhenna/Desktop/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='eraser2'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO business (business_id, name, address,state,city,zipcode,latitude,longitude,stars,review_count,num_checkins,is_open) " \
                      "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + cleanStr4SQL(data["postal_code"]) + "'," + str(data["latitude"]) + "," + \
                      str(data["longitude"]) + "," + str(data["stars"]) + "," + str(data["review_count"]) + ",0 ,"  + \
                      int2BoolStr(data["is_open"]) + ");"
            try:
                cur.execute(sql_str)
            except psycopg2.Error as e: 
                print("Insert to user table failed...")
                print("Error: ", e)
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insertUser():
    with open('C:/Users/Jhenna/Desktop/yelp_user.JSON', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='eraser2'")
        except:
            print('Cant connect to db!')
        
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO acc (average_stars, fans, cool, funny, name, review_count, useful, yelping_since, user_id) " \
                "VALUES ('" + str(data['average_stars']) + "','" + str(data["fans"]) + "','" + str(data["cool"]) + "','" \
                + str(data["funny"]) + "','" + cleanStr4SQL(data["name"]) + "','" + str(data["review_count"]) + "','" + str(data["useful"]) \
                + "','" + cleanStr4SQL(data["yelping_since"])  + "','" + str(data["user_id"]) + "') " \
                + "ON CONFLICT (user_id) DO NOTHING;"
            try:
                cur.execute(sql_str)
            except psycopg2.Error as e: 
                print("Insert to user table failed...")
                print("Error: ", e)
            conn.commit()

            line = f.readline()
            count_line += 1

        cur.close()
        conn.close()
    print(count_line)
    f.close()

def insertReviews():
    #reading the JSON file
    with open('C:/Users/Jhenna/Desktop/yelp_review.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='eraser2'")
        except:
            print('Unable to connect to the database!')
            
def insertCategories():
    with open('C:/users/sinou/Desktop/yelp_business.json', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='pass'")

        except:
            print("Error: cannot connect")
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            
            categ = data['categories']
            for cat in categ:
                sql_str = "INSERT INTO categories(business_id, category_name) " + \
                   "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(cat) + "');"
                try:
                    cur.execute(sql_str)
                except psycopg2.Error as e: 
                    print("Insert to user table failed...")
                    print("Error: ", e)
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insertHours():
    with open('C:/users/sinou/Desktop/yelp_business.json', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='pass'")

        except:
            print("Error: cannot connect")
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO review(review_id, user_id, business_id, stars, date, " \
                            "cool_vote, funny_vote, useful_vote, text) " \
                            "VALUES ('" + cleanStr4SQL(data['review_id']) + "','" + \
                            cleanStr4SQL(data["user_id"]) + "','" + cleanStr4SQL(data["business_id"]) \
                            + "'," + str(data["stars"]) + ",'" + cleanStr4SQL(data["date"]) + "'," + \
                            str(data["cool"]) + "," + str(data["funny"]) + "," + \
                            str(data["useful"]) + ",'" + cleanStr4SQL(data["text"]) + "');"
            
            hours = data['hours']
            for days in hours:
                sql_str = "INSERT INTO hours (business_id, day, open, close) VALUES ('" + \
                    cleanStr4SQL(data['business_id']) + "','" + days + "','" + \
                        hours[days].split("-")[0] + "','" + hours[days].split("-")[1] + "');"

                try:
                    cur.execute(sql_str)
                except psycopg2.Error as e: 
                    print("Insert to user table failed...")
                    print("Error: ", e)
                conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insertFriends():
    with open('C:/users/sinou/Desktop/yelp_user.json', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
           # conn = pscyopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='Sweety12'")
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='Sweety12'")

        except:
            print("Error: cannot connect to database")
        cur = conn.cursor()

        while line:
            data = json.loads(line)

            friends = data['friends']

            for friend in friends:
                sql_str = "INSERT INTO friends(user_id, friend_id) " + \
                    "VALUES ('" + cleanStr4SQL(data['user_id']) + "','" + friend + "') " + \
                    "ON CONFLICT(friend_id) DO NOTHING;" 

            try:
                cur.execute(sql_str)
            except psycopg2.Error as e: 
                print("Insert to user table failed...")
                print("Error: ", e)
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()


#insert2BusinessTable()
#insertUser()
insertReviews()

def insertCheckins():
    with open('C:/users/sinou/Desktop/yelp_checkin.JSON', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='Sweety12'")
        except:
            print("Can't connect to db")
        
        cur = conn.cursor()

        while line:
            data = json.loads(line)

            business_id = str(cleanStr4SQL(data['business_id']))
           # for day in data['time']:
            #    for k,v in data['time'][day].items():
            #        sql_str = "INSERT INTO checkins (business_id, day, time, count) " \
            #            +"VALUES ('" + str(cleanStr4SQL(data['business_id'])) + "','" + str(day) + "','" + str(k) + "','" + str(v) + "');"
            for day in data['time']:
                for hour in data['time'][day].keys():
                    sql_str = "INSERT INTO checkins (business_id, day, time, count) " \
                    + "VALUES ('" + str(business_id) + "','" + str(day) + "','" + str(hour) + "','" + str(data['time'][day][hour]) + "');"

                    try:
                        cur.execute(sql_str)
                    except psycopg2.Error as e: 
                        print("Insert to user table failed...")
                        print("Error: ", e)
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()




#
# insert2BusinessTable()
