CREATE TRIGGER ReviewCountIncrement
	AFTER INSERT ON reviews
	FOR EACH ROW 
		SET num_revs = num_revs + 1;
		
CREATE TRIGGER ReviewCountDecrement
	AFTER DELETE ON reviews
	FOR EACH ROW
		SET num_revs = num_revs - 1;
		
CREATE TRIGGER UpdateReviewRating
	AFTER INSERT ON reviews
		SET avg_rev = 
			SUM(SELECT stars FROM reviews WHERE reviews.busines_id = business_id) /
			COUNT (SELECT * FROM reviews WHERE reviews.business_id = business_id);		