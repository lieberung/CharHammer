SELECT row_to_json(t) from (
SELECT lieutypeid AS id, fk_parentid AS parentid, lieutypelibelle AS libelle
	FROM warhammer.lieutype
	) t