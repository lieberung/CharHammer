SELECT row_to_json(t) from (
SELECT talentid AS id
	, talentparentkey AS parent_id
	, talentignore AS ignorer
	, talenttrait AS trait
	, talentlibelle AS nom
	, COALESCE(talentresume,'') AS resume
	, COALESCE(talentdescription,'') AS description
	, COALESCE(talentspecialis,'') AS specialisation
	FROM warhammer.talent
	) t;