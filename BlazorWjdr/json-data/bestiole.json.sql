SELECT row_to_json(t) from (
SELECT bestioleid AS id, fk_profilactuelid, fk_userid, bestiolenom AS nom, bestiolehistoire AS histoire, bestiolecomment AS comment
	, fk_bestiolecompetences, fk_bestioletalents, fk_origines, fk_raceid, bestiolemembrede AS membrede
	, bestiolepoid AS poids, bestioletaille AS taille, bestioleage AS age, bestiolesexe AS sexe, bestiolepsycho AS psycho
	FROM warhammer.bestiole
	) t