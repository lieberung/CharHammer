SELECT row_to_json(t) from (
SELECT choixcompetenceid AS id, choixcompetencekeys
	FROM warhammer.choixcompetence
	) t