import * as React from 'react'

import { BuyWorkerButton } from './BuyWorkerButton/BuyWorkerButton'

import {
	COST_WORKER,
	PRODUCTION_WORKER
} from '../store/Idler'

const WorkerDisplay = ({ buyWorker, numResource, numWorkers }) => (
	<section>
		<h2>Workers: {numWorkers}</h2>
		<section>
			<BuyWorkerButton
				buyWorker={buyWorker}
				cost={COST_WORKER}
				disabled={numResource < COST_WORKER}
				production={PRODUCTION_WORKER} />
		</section>
	</section>
)

export { WorkerDisplay }
