SELECT row_to_json(t) from (
SELECT carriereid AS id
	, fk_sourceid
	, fk_parentcarriereid
	, fk_plandecarriereid
	, fk_debouches
	, fk_competences
	, fk_talents
	, fk_choixcompetences
	, fk_choixtalents
	, carriereavancee AS avancee
	, carrierecomplete AS complete
	, carrierelibelle AS libelle
	, coalesce(carrieredescription,'') AS description
	, coalesce(carriereimage,'') AS image
	, coalesce(carriererestriction,'') AS restriction
	, coalesce(carrieresource,'') AS source
	, coalesce(carrieredotations,'') AS dotations
	FROM warhammer.carriere
	) t