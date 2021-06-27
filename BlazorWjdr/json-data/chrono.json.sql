SELECT row_to_json(t) from (
SELECT chronoid AS id, fk_userid, chronodebut AS debut, chronofin AS fin
	, chronoresume AS resume, chronotitre AS titre, chronocomment AS comment
	, chronosources AS sources, chronodomaines AS domaines
	FROM warhammer.chrono
	) t