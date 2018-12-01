import * as React from 'react'

import './BuyWorkerButton.css'

const BuyWorkerButton = ({
	buyWorker,
	cost,
	disabled,
	production }) =>
		<button className="buy-worker-button" disabled={disabled}
			onClick={buyWorker}>Buy Worker
				<em className="negative-number">Cost={cost}</em>
				<em className="positive-number">Prod={production}</em>
			</button>

export { BuyWorkerButton }
