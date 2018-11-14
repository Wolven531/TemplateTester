const alterByAmountType = 'alter_by_amount'
const incrementCountType = 'increment_count'
const decrementCountType = 'decrement_count'

const initialState = { count: 0 }

const actionCreators = {
	alterByAmount: (deltaAmount) => ({
		type: alterByAmountType,
		payload: deltaAmount
	}),
	decrement: () => ({ type: decrementCountType }),
	increment: () => ({ type: incrementCountType })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	const { count } = state

	switch (type) {
		case alterByAmountType:
			return { ...state, count: count + payload }
		case decrementCountType:
			return { ...state, count: count - 1 }
		case incrementCountType:
			return { ...state, count: count + 1 }
		default:
			break;
	}

	return state
}

export {
	actionCreators,
	reducer
}
