import * as React from 'react'
import { connect } from 'react-redux'
// import { bindActionCreators, dispatch } from 'redux'

import { actionCreators } from '../store/Entities'

const EntityViewer = ({ entitiesReducer }) => {
	const { entities } = entitiesReducer

	return <div>
		<h2>Entity Viewer</h2>
		{entities.length === 0
			? <p>No entities stored</p>
			: <ul>{entities.map((entity, ind) =>
					<li key={`entity-${ind}`}>
						<pre>{JSON.stringify(entity, null, 4)}</pre>
					</li>)}
			</ul>}
	</div>
}

const mapStateToProps = state => {
	console.info(`[ mapStateToProps | EntityViewer ]`)
	// NOTE: pull entities reducer state out of overall state tree
	const { entities } = state

	return { entitiesReducer: entities }
}

const mapDispatchToProps = dispatch => {
	console.info(`[ mapDispatchToProps | EntityViewer ]`)
	return {
		addEntity: (newEntity) => {
			dispatch(actionCreators.addEntity(newEntity))
		},
		getAllEntities: () => {
			dispatch(actionCreators.getAllEntities())
		}
	}
}

export default connect(
	mapStateToProps,
	mapDispatchToProps
	// dispatch => bindActionCreators(actionCreators, dispatch)
)(EntityViewer)
