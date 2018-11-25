import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { actionCreators } from '../store/Entities'

class SimpleEntityAdder extends React.Component {
	constructor(props) {
		super(props)
		this.state = {
			newEntityText: ''
		}
	}

	render() {
		return (
			<div>
				<h2>Entity Adder</h2>
				<div>
					<label htmlFor="newEntityText">New Entity</label>
					<br/>
					<textarea
						id="newEntityText"
						name="newEntityText"
						value={this.state.newEntityText}
						onChange={this.handleNewEntityTextChange} />
				</div>
				<button onClick={this.handleAddEntityClick}>Add Entity</button>
			</div>
		)
	}

	handleAddEntityClick = (clickEvent) => {
		this.props.addEntity({ userInput: this.state.newEntityText })
		this.setState({ newEntityText: '' })
	}

	handleNewEntityTextChange = (changeEvent) => {
		if (!changeEvent || !changeEvent.target) {
			console.warn(
				'[handleNewEntityTextChange] Cannot continue change event - missing target',
				changeEvent,
				changeEvent.target)
			return
		}
		this.setState({
			newEntityText: changeEvent.target.value
		})
	}
}

const EntityAdder = connect(
	state => state.entities,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(SimpleEntityAdder)

export { EntityAdder }
