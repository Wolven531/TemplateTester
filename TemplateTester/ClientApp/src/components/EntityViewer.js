import * as React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'

import { actionCreators } from '../store/Entities'

const statelessEntityViewer = ({ entitiesReducer }) => {
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

const EntityViewer = connect(
	state => ({ entitiesReducer: state.entities }),
	dispatch => bindActionCreators(actionCreators, dispatch)
)(statelessEntityViewer)

export { EntityViewer }
