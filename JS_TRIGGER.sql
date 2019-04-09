CREATE FUNCTION check_inc()
	RETURNS TRIGGER AS $inccheck$
BEGIN
	UPDATE business
	SET num_checkins = num_checkins + 1
	WHERE business_id = NEW.business_id;
	RETURN NEW;
END;
$inccheck$ LANGUAGE plpgsql;


CREATE TRIGGER CheckinInc AFTER INSERT 
ON checkins
	FOR EACH ROW
	EXECUTE PROCEDURE check_inc();

CREATE FUNCTION check_dec()
	RETURNS TRIGGER AS $deccheck$
BEGIN
	UPDATE business
	SET num_checkins = num_checkins - 1
	WHERE business_id = NEW.business_id;
	RETURN NEW;
END;
$deccheck$ LANGUAGE plpgsql;


CREATE TRIGGER CheckinDec AFTER DELETE 
ON checkins
	FOR EACH ROW
	EXECUTE PROCEDURE check_dec();

CREATE FUNCTION rev_inc()
	RETURNS TRIGGER AS $increv$
	BEGIN
		UPDATE business
		SET review_count = review_count + 1
		WHERE business_id = NEW.business_id;
		RETURN NEW;
	END;
	$increv$ LANGUAGE plpgsql;

CREATE TRIGGER revInc AFTER INSERT
	ON review
	FOR EACH ROW
	EXECUTE PROCEDURE checkin_dec();

CREATE FUNCTION rev_dec()
	RETURNS TRIGGER AS $decrev$
	BEGIN
		UPDATE business
		SET review_count = review_count - 1
		WHERE business_id = NEW.business_id;
		RETURN NEW;
	END;
	$decrev$ LANGUAGE plpgsql;

CREATE TRIGGER revDec AFTER DELETE
	ON review
	FOR EACH ROW
	EXECUTE PROCEDURE rev_dec();

CREATE FUNCTION revAvg()
	RETURNS TRIGGER AS $avg$
	BEGIN
		UPDATE business
		SET reviewRating = (SELECT AVG(stars) FROM review
						   	WHERE review.business_id = business.business_id)
		WHERE business_id = NEW.business_id;
		RETURN NEW;
	END;
	$avg$ LANGUAGE plpgsql;

CREATE TRIGGER avgUpd
	AFTER INSERT ON review
	FOR EACH ROW
	EXECUTE PROCEDURE revAvg();
	