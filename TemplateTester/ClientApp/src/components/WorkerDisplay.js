import * as React from 'react'

import { BuyWorkerButton } from './BuyWorkerButton/BuyWorkerButton'

import { PRODUCTION_WORKER } from '../store/Idler'

const WorkerDisplay = ({ buyWorker, numResource, numWorkers, workerCost }) => (
	<section>
		<h2>Workers: {numWorkers}</h2>
		<section>
			<BuyWorkerButton
				buyWorker={buyWorker}
				cost={workerCost}
				disabled={numResource < workerCost}
				production={PRODUCTION_WORKER} />
		</section>
	</section>
)

export { WorkerDisplay }
