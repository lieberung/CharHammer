SELECT row_to_json(t) from (
SELECT raceid AS id
	, fk_parentid AS parent_id
	, fk_profilid AS profil_id
	, fk_lieux AS lieux_ids
	, racepourpj AS pour_pj
	, racepourpnj AS pour_pnj
	, racegrouponly AS group_only
	, COALESCE(racenommasculin,'') AS nom_masculin
	, COALESCE(racenomfeminin,'') AS nom_feminin
	, COALESCE(racedescription,'') AS description
	, COALESCE(racetraits,'') AS traits
	, COALESCE(raceimagemale,'') AS image_male
	, COALESCE(raceimagefemelle,'') AS image_femelle
	FROM warhammer.race
) t;