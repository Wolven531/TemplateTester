const ADD_ENTITY = 'add_entity'
const CLEAR_ALL_ENTITIES = 'clear_all_entities'
const GET_ALL_ENTITIES = 'get_all_entities'
const LOAD_ENTITIES_LOCAL = 'load_entities_local'
const SAVE_ENTITIES_LOCAL = 'save_entities_local'

const LOCAL_STORAGE_KEY_ENTITIES = 'awill.reducers.Entities.entities'

const loadFromLocalStorage = () => {
	const entitiesString = localStorage.getItem(LOCAL_STORAGE_KEY_ENTITIES)
	if (!entitiesString) {
		console.warn('localStorage did not contain stored entities, clearing...')
		return []
	}
	return JSON.parse(entitiesString)
}

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
	clearAllEntities: () => ({ type: CLEAR_ALL_ENTITIES }),
	getAllEntities: () => ({ type: GET_ALL_ENTITIES }),
	loadEntitiesLocalStorage: () => ({ type: LOAD_ENTITIES_LOCAL }),
	saveEntitiesLocalStorage: () => ({ type: SAVE_ENTITIES_LOCAL })
}

const reducer = (state, action) => {
	state = state || initialState

	const { payload, type } = action
	// console.info(`[reducer | entities] type=${type}`)

	switch (type) {
		case ADD_ENTITY:
			entities.splice(entities.length, 0, payload.newEntity)
			return { ...state, entities }
		case CLEAR_ALL_ENTITIES:
			entities.splice(0, entities.length)
			return { ...state, entities }
		case GET_ALL_ENTITIES:
			break;
		case LOAD_ENTITIES_LOCAL:
			entities.splice(0, entities.length, ...loadFromLocalStorage())
			return { ...state, entities }
		case SAVE_ENTITIES_LOCAL:
			localStorage.setItem(LOCAL_STORAGE_KEY_ENTITIES, JSON.stringify(entities))
		default:
			break;
	}

	return state
}

export {
	actionCreators,
	reducer
}
