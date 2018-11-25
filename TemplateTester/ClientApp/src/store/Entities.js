const ADD_ENTITY = 'add_entity'
const GET_ALL_ENTITIES = 'get_all_entities'

const entities = [
	{ asdf: 'qwer', someNum: -5.43 },
	{ id: '70004d80-ee09-4884-a288-9e6b92798ad3', name: 'Shape B', isMaxed: true },
	{ relatedThings: [{ content: 'thing1' }, { content: 'thing2' } ] }
]

const initialState = { entities }

const actionCreators = {
	addEntity: (newEntity) => ({
		type: ADD_ENTITY,
		payload: { newEntity }
	}),
	getAllEntities: () => ({ type: GET_ALL_ENTITIES })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	// console.info(`[reducer | entities] type=${type}`)

	switch (type) {
		case ADD_ENTITY:
			//entities.splice(entities.length, 0, payload.newEntity)
			entities.push(payload.newEntity)
			return { ...state, entities }
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
