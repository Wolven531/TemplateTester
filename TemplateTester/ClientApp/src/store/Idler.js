import { Upgrades } from '../models/Upgrades'

const BUY_UPGRADE = 'buy_upgrade'
const BUY_WORKER = 'buy_worker'
const COLLECT_INCOME = 'collect_income'
const GENERATE_RESOURCE = 'generate_resource'
// const START_INCOME_TIMER = 'start_income_timer'
// const STOP_INCOME_TIMER = 'stop_income_timer'

const STARTING_COST_WORKER = 10

const INCOME_FREQUENCY_MILLISECONDS = 1000

const PRODUCTION_WORKER = 1

const initialState = {
	// incomeTimer: null,
	numResource: 0,
	numWorkers: 0,
	workerCost: STARTING_COST_WORKER
}

const actionCreators = {
	buyUpgrade: upgradeName => (
		{
			type: BUY_UPGRADE,
			payload: {
				upgradeName
			}
		}),
	buyWorker: () => ({ type: BUY_WORKER }),
	collectIncome: () => ({ type: COLLECT_INCOME }),
	generateResource: () => ({ type: GENERATE_RESOURCE })
	// startIncomeTimer: () => ({ type: START_INCOME_TIMER }),
	// stopIncomeTimer: () => ({ type: STOP_INCOME_TIMER })
}

const reducer = (state, action) => {
	const { payload, type } = action
	state = state || initialState

	let {
		// incomeTimer,
		numResource,
		numWorkers,
		workerCost
	} = state

	switch (type) {
		case BUY_UPGRADE:
			const { upgradeName } = payload
			const upgrade = Upgrades[upgradeName]

			console.log(`[ reducer | Idler ] upgrade=${JSON.stringify(upgrade, null, 4)}`)

			return {
				...state
			}
		case BUY_WORKER:
			// NOTE: decrease resources
			numResource -= workerCost
			// NOTE: increase number of workers
			numWorkers++
			// NOTE: increase worker cost
			workerCost = parseInt((numWorkers / 5.0) + (1.1 * workerCost), 10)

			return {
				...state,
				numResource,
				numWorkers,
				workerCost
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
	STARTING_COST_WORKER as COST_WORKER,
	INCOME_FREQUENCY_MILLISECONDS,
	PRODUCTION_WORKER,
	actionCreators,
	reducer
}
