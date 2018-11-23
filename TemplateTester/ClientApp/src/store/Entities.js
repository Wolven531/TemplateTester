const GET_ALL_ENTITIES = 'get_all_entities'

const initialState = {
	entities: [
		{ asdf: 'qwer', someNum: -5.43 },
		{ id: '70004d80-ee09-4884-a288-9e6b92798ad3', name: 'Shape B', isMaxed: true },
		{ relatedThings: [{ content: 'thing1' }, { content: 'thing2' } ] }
	]
}

const actionCreators = {
	getAllEntities: () => ({ type: GET_ALL_ENTITIES })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	const { entities } = state

	switch (type) {
		case GET_ALL_ENTITIES:
			return { ...state, entities }
		default:
			break;
	}

	return state
}

export {
	actionCreators,
	reducer
}
