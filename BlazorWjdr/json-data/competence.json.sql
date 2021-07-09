SELECT row_to_json(t) from (
SELECT competenceid AS id
	, fk_talentslies
	, fk_competencemereid
	, competenceignore AS ignorer
	, competencebase AS de_base
	, competencecaract AS carac
	, competencelibelle AS nom
	, COALESCE(competenceresume,'') AS resume
	, COALESCE(competencespecialis,'') AS specialisation
	FROM warhammer.competence
	) t;