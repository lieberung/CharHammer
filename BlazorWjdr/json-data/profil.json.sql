SELECT row_to_json(t) from (
SELECT profilid AS id
	, profilcc AS cc
	, profilct AS ct
	, profilf AS f
	, profile AS e
	, profilag AS ag
	, profilint AS intel
	, profilfm AS fm
	, profilsoc AS soc
	, profila AS a
	, profilb AS b
	, profilbf AS bf
	, profilbe AS be
	, profilm AS m
	, profilmag AS mag
	, profilpf AS pf
	, profilpd AS pd
	FROM warhammer.profil
	) t;