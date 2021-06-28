SELECT row_to_json(t) from (
SELECT calcpointssid AS id, fk_raceid, calcpointsdicevalue AS dicevalue, calcpointsblessures AS blessures
	, calcpointsdestin AS destin, calcpointssiblings AS siblings, calcpointscheveux AS cheveux, calcpointsyeux AS yeux
	FROM warhammer.tablecalcpoints
	) t;