SELECT row_to_json(t) from (
SELECT seanceid AS id, fk_teamid, fk_campainid, seancedate AS date, seanceduree AS duree, seancetitre AS titre
	, seancexp AS xp, seancexpcomment AS xp_comment, seancesumup as resume, seanceacte AS acte
	FROM warhammer.seance
	) t;