SELECT row_to_json(t) from (
SELECT graveid AS id, graveuserid AS user_id, graveepitaph AS epitaph, gravelastwords AS last_words, gravelandlord AS land_lord
	FROM graveyard.grave
	) t;