UPDATE business
SET review_count = (SELECT COUNT(*)
                    FROM review r
                    WHERE business.business_id = r.business_id);

UPDATE business 
SET num_checkins = (SELECT SUM(count)
                    FROM checkins c
                    WHERE business.business_id = c.business_id);

UPDATE business
SET reviewRating = (SELECT AVG(stars)
                    FROM review r
                    WHERE business.business_id = r.business_id);