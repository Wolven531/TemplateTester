const BUY_WORKER = 'buy_worker'
const COLLECT_INCOME = 'collect_income'
const GENERATE_RESOURCE = 'generate_resource'
// const START_INCOME_TIMER = 'start_income_timer'
// const STOP_INCOME_TIMER = 'stop_income_timer'

const COST_WORKER = 10

const INCOME_FREQUENCY_MILLISECONDS = 1000

const PRODUCTION_WORKER = 1

const initialState = {
	// incomeTimer: null,
	numResource: 0,
	numWorkers: 0
}

const actionCreators = {
	buyWorker: () => ({ type: BUY_WORKER }),
	collectIncome: () => ({ type: COLLECT_INCOME }),
	generateResource: () => ({ type: GENERATE_RESOURCE })
	// startIncomeTimer: () => ({ type: START_INCOME_TIMER }),
	// stopIncomeTimer: () => ({ type: STOP_INCOME_TIMER })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	let {
		// incomeTimer,
		numResource,
		numWorkers
	} = state

	switch (type) {
		case BUY_WORKER:
			numResource -= COST_WORKER
			numWorkers++

			return {
				...state,
				numResource,
				numWorkers
			}
		case COLLECT_INCOME:
			numResource += numWorkers * PRODUCTION_WORKER

			return { ...state, numResource }
		case GENERATE_RESOURCE:
			return { ...state, numResource: numResource + 1 }
		// case START_INCOME_TIMER:
		// 	if (incomeTimer) {
		// 		// console.info(`[ reducer | Idler ] Clearing income timer ${incomeTimer}...`)
		// 		clearInterval(incomeTimer)
		// 	}
		// 	incomeTimer = setInterval(() => {
		// 			console.info('[ incomeTimerElapsed | reducer | Idler ] Should be collecting income...')
		// 	}, INCOME_FREQUENCY_MILLISECONDS)
		// 	// console.info(`[ reducer | Idler ] Started income timer ${incomeTimer}`)
		// 	return { ...state, incomeTimer }
		// case STOP_INCOME_TIMER:
		// 	return { ...state }
		default:
			break
	}

	return state
}

export {
	COST_WORKER,
	INCOME_FREQUENCY_MILLISECONDS,
	PRODUCTION_WORKER,
	actionCreators,
	reducer
}
