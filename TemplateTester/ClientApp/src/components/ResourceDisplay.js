import * as React from 'react'

import { PRODUCTION_WORKER } from '../store/Idler'

const ResourceDisplay = ({
	numResource,
	numWorkers,
	percentToNextPayday
}) => (
	<section>
		<h2>Resources: {numResource}</h2>
		{numWorkers > 0 &&
			<article>
				<label htmlFor="incomeProgress">Payday <em className="positive-number">+ {numWorkers * PRODUCTION_WORKER}</em>:</label>
				<progress id="incomeProgress" value={percentToNextPayday} max={100} />
			</article>}
	</section>
)

export { ResourceDisplay }
