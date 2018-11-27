const ADD_WORKER = 'add_worker'
const GENERATE_RESOURCE = 'generate_resource'

const initialState = {
	numResource: 0,
	numWorkers: 0
}

const actionCreators = {
	addWorker: () => ({ type: ADD_WORKER }),
	generateResource: () => ({ type: GENERATE_RESOURCE })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	const { numResource, numWorkers } = state

	switch (type) {
		case ADD_WORKER:
			return { ...state, numWorkers: numWorkers + 1 }
		case GENERATE_RESOURCE:
			return { ...state, numResource: numResource + 1 }
		default:
			break
	}

	return state
}

export {
	actionCreators,
	reducer
}
