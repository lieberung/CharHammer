SELECT row_to_json(t) from (
SELECT teamid AS id, teamname AS nom
	FROM warhammer.team
	) t;