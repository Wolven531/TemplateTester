import React from 'react'
import { connect } from 'react-redux'

import { EntityAdder } from './EntityAdder'
import { EntityViewer } from './EntityViewer'
import { bindActionCreators } from 'redux'
import { actionCreators } from '../store/Entities'

class SimpleEntityManager extends React.Component {
	render() {
		return (
			<div>
				<h2>Entity Manager</h2>
				<EntityAdder />
				<EntityViewer />
				<button
					onClick={this.handleClearEntitiesClick}>Clear Entities</button>
			</div>
		)
	}

	handleClearEntitiesClick = (evt) => {
		this.props.clearAllEntities()
	}
}

const EntityManager = connect(
	state => state.entities,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleEntityManager)

export { EntityManager }
