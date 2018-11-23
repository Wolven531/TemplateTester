import React from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { actionCreators } from '../store/Entities'

class EntityViewer extends React.Component {
	render() {
		return (
			<div>
				<h2>Entity Viewer</h2>
				{this.props.entities.length === 0
					? <p>No entities stored</p>
					: <ul>{this.props.entities.map((entity, ind) =>
							<li key={`entity-${ind}`}>
								<pre>{JSON.stringify(entity, null, 4)}</pre>
							</li>)}
					</ul>}
			</div>
		)
	}
}

export default connect(
	state => state.entities,
	dispatch => bindActionCreators(actionCreators, dispatch)
)(EntityViewer)
