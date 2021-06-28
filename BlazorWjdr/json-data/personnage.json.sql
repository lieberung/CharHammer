SELECT row_to_json(t) from (
SELECT personnageid AS id, fk_carrierepereid, fk_carrieremereid, fk_signeastralid
	, personnagenbfreressoeurs AS freres_et_soeurs, personnagemaindir AS main_directrice, personnagemort AS mort
	, fk_cheminprofess, personnagecheveux AS cheveux, personnageyeux AS yeux
	FROM warhammer.personnage
	) t;