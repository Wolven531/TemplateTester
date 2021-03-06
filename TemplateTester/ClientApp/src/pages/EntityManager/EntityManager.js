import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { EntityAdder } from '../../components/EntityAdder'
import { EntityViewer } from '../../components/EntityViewer'

import { actionCreators } from '../../store/Entities'

class SimpleEntityManager extends React.Component {
	componentDidMount() {
		window.document.title = 'Entity Manager | Web UI'
	}

	render() {
		return (
			<div>
				<h2>Entity Manager</h2>
				<EntityAdder />
				<EntityViewer />
				<button onClick={this.handleClearEntitiesClick}>Clear Entities</button>
				<button onClick={this.handleSaveEntitiesClick}>Save Entities (Local Storage)</button>
				<button onClick={this.handleLoadEntitiesClick}>Load Entities (Local Storage)</button>
			</div>
		)
	}

	handleClearEntitiesClick = (evt) => this.props.clearAllEntities()

	handleLoadEntitiesClick = (evt) => this.props.loadEntitiesLocalStorage()

	handleSaveEntitiesClick = (evt) => this.props.saveEntitiesLocalStorage()
}

const EntityManager = connect(
	state => state.entities,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleEntityManager)

export { EntityManager }
