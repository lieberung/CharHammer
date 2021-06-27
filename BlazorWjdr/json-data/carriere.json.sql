SELECT row_to_json(t) from (
SELECT carriereid AS id, carrierelibelle AS libelle, carrieredescription AS description
	, carriereimage AS image, carriereavancee AS avancee, carriererestriction AS restriction
	, fk_plandecarriereid, carrieresource AS source
	, fk_debouches, fk_competences, fk_talents
	, fk_choixcompetences, fk_choixtalents
	, fk_sourceid
	, carrierecomplete AS complete
	, carrieredotations AS dotations
	, fk_parentcarriereid
	FROM warhammer.carriere
	) t