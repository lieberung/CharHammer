SELECT row_to_json(t) from (
SELECT pjid AS id, pjdatecreation AS date_creation, pjnomjoueur as nom_joueur
	, pjxpactuel as xp_actuel, pjxptotal as xp_total, fk_profilinitialid
	FROM warhammer.pj
) t;