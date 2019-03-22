import python
import psycopg2
def insert2BusinessTable():
    #reading the JSON file
    with open('C:/users/sinou/Desktop/yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='pass'")
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
    with open('C:/users/sinou/Desktop/yelp_user.JSON', 'r') as f:
        line = f.readline()
        count_line = 0

        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='pass'")
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


#
# insert2BusinessTable()