SELECT row_to_json(t) from (
SELECT dieuid AS id
	, fk_dieupatron AS patron_id
	, dieunom AS nom
	, COALESCE(dieudomaines,'') AS domaines
	, COALESCE(dieufideles,'') AS fideles
	, COALESCE(dieusymboles,'') AS symboles
	, COALESCE(dieusymbolesimg,'') as symboles_image
	, COALESCE(dieuhistoire,'') AS histoire
	, COALESCE(dieucomment,'') AS comment
  FROM warhammer.dieu
	) t;