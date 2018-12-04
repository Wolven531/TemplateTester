import * as React from 'react'

import { PRODUCTION_WORKER } from '../store/Idler'

const ResourceDisplay = ({
	generateResource,
	numResource,
	numWorkers,
	percentToNextPayday
}) => (
	<section className="resource-display">
		<h2>Resources: {numResource}</h2>
		{numWorkers > 0 &&
			<article>
				<label htmlFor="incomeProgress">Payday <em className="positive-number">+ {numWorkers * PRODUCTION_WORKER}</em>:</label>
				<progress id="incomeProgress" value={percentToNextPayday} max={100} />
			</article>}
			<button
				className="generate-resource-button"
				onClick={generateResource}>
				Generate Resource <em className="positive-number">Prod=1</em>
			</button>
	</section>
)

export { ResourceDisplay }
