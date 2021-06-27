SELECT row_to_json(t) from (
SELECT domaineid AS id, domainenom AS nom, domainecomment AS comment
	FROM warhammer.domaine
	) t;