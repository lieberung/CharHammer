SELECT row_to_json(t) from (
SELECT userid AS id, useremail AS email, userpseudo AS pseudo, userpassword AS password
	FROM graveyard."user"
	) t;