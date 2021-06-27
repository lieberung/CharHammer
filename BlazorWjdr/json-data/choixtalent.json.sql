SELECT row_to_json(t) from (
SELECT choixtalentid AS id, choixtalentkeys
	FROM warhammer.choixtalent
	) t