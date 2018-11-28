const BUY_WORKER = 'buy_worker'
const GENERATE_RESOURCE = 'generate_resource'

const COST_WORKER = 10

const initialState = {
	numResource: 0,
	numWorkers: 0
}

const actionCreators = {
	buyWorker: () => ({ type: BUY_WORKER }),
	generateResource: () => ({ type: GENERATE_RESOURCE })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	let { numResource, numWorkers } = state

	switch (type) {
		case BUY_WORKER:
			numResource -= COST_WORKER
			numWorkers++

			return {
				...state,
				numResource,
				numWorkers
			}
		case GENERATE_RESOURCE:
			return { ...state, numResource: numResource + 1 }
		default:
			break
	}

	return state
}

export {
	COST_WORKER,
	actionCreators,
	reducer
}
