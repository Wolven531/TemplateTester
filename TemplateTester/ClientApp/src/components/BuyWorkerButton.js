import * as React from 'react'

const BuyWorkerButton = ({
	buyWorker,
	cost,
	disabled,
	production }) =>
		<button disabled={disabled}
			onClick={buyWorker}>Buy Worker ( Cost={cost} Prod={production} )</button>

export { BuyWorkerButton }
