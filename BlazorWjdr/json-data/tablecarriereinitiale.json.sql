SELECT row_to_json(t) from (
SELECT carriereinitialeid AS id, fk_raceid, fk_carriereid, carriereinitialefacteur AS facteur
	FROM warhammer.tablecarriereinitiale
	) t